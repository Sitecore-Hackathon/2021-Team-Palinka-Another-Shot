import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { fetchWorkItems, postChangeWorkflow, workItemsSelector } from "../redux/slices/workitems";
import { DragDropContext } from "react-beautiful-dnd";
import Column from "./Column";

const Board = () => {
  const dispatch = useDispatch();
  const [data, setData] = useState(null);
  const [itemToComment, setItemToComment] = useState({});
  const [commentErrorMessage, setCommentErrorMessage] = useState("");
  const [commentBoxVisible, setCommentBoxVisibility] = useState(false);
  const [isLoading, setLoading] = useState(false);
  const [enabledStateIds, setEnabledIds] = useState([0, []]);
  const { workItems } = useSelector(workItemsSelector);
  const { selectedWorkflowId } = useSelector(workItemsSelector);

  const commentTextAreaRef = React.createRef();

  useEffect(() => {
    if (workItems && workItems.States) {
      setData(workItems);
    }
  }, [workItems, selectedWorkflowId]);

  const reOrderItems = (originalItems, reOrdered) => {
    const orderedItems = [];
    reOrdered.forEach(ID => {
      let tempArray = originalItems.filter(item => {
        return item.ID === ID;
      });
      orderedItems.push(tempArray[0]);
    });
    return orderedItems;
  };

  const removeItemById = (originalItems, id) => {
    return originalItems.filter(item => {
      return item.ID !== id;
    });
  };

  const getNextStateIds = (itemId, stateId) => {
    const state = data.States.filter(state => {
      return state.Id === stateId;
    })[0];

    const item = state.Items.filter(item => {
      return item.ID === itemId;
    })[0];

    const nextStateIds = [];

    item.NextStates.forEach(nextState => {
      nextStateIds.push(nextState.Id);
    });

    nextStateIds.push(stateId);

    return nextStateIds;
  };

  const getActionAndLang = (targetStateId, sourceStateId, itemId) => {
    if (targetStateId === sourceStateId) {
      return;
    }

    const sourceState = data.States.filter(state => {
      return state.Id === sourceStateId;
    })[0];

    const activeItem = sourceState.Items.filter(item => {
      return item.ID === itemId;
    })[0];

    const lang = activeItem.Language;
    const action = activeItem.NextStates.filter(next => {
      return next.Id === targetStateId;
    })[0].Actions[0];

    return {
      action,
      lang,
    };

  };

  const updateBoardStates = (destination, source, draggableId) => {
    const start = data.States.filter(obj => {
      return obj.Id === source.droppableId;
    })[0];
    const finish = data.States.filter(obj => {
      return obj.Id === destination.droppableId;
    })[0];

    if (start === finish) {
      const newItemIds = start.Items.map(item => item.ID);
      newItemIds.splice(source.index, 1);
      newItemIds.splice(destination.index, 0, draggableId);

      const newStartItems = reOrderItems(start.Items, newItemIds);
      const newColumn = {
        ...start,
        Items: newStartItems,
      };

      const idx = data.States.findIndex(item => item.Id === start.Id);
      const updatedStates = [...data.States.slice(0, idx), newColumn, ...data.States.slice(idx + 1)];

      const newState = {
        ...data,
        States: [
          ...updatedStates
        ],
      };

      setData(newState);
      return;
    }

    // Moving from one list to another
    const startItemIds = start.Items.map(item => item.ID);
    const idToRemove = startItemIds.splice(source.index, 1)[0];
    const filteredStart = removeItemById(start.Items, idToRemove);
    const removedItem = start.Items.filter(item => {
      return item.ID === idToRemove;
    });

    const newStartColumn = {
      ...start,
      Items: filteredStart,
    };

    const finishedItems = Array.from(finish.Items);
    finishedItems.splice(destination.index, 0, removedItem[0]);

    const newFinishedColumn = {
      ...finish,
      Items: finishedItems,
    };

    const idxStart = data.States.findIndex(item => item.Id === start.Id);
    const idxFinish = data.States.findIndex(item => item.Id === finish.Id);
    const updatedStates = [...data.States.slice(0, idxStart), newStartColumn, ...data.States.slice(idxStart + 1)];
    const updatedStates2 = [...updatedStates.slice(0, idxFinish), newFinishedColumn, ...updatedStates.slice(idxFinish + 1)];

    const newState = {
      ...data,
      States: [
        ...updatedStates2
      ],
    };

    setData(newState);
  };

  const getRealId = (id) => {
    return id.split("-lang-")[0];
  };

  const onDragStart = start => {
    setEnabledIds(
      [
        start.source.droppableId,
        getNextStateIds(getRealId(start.draggableId), start.source.droppableId)
      ]
    );
  };

  const onDragEnd = result => {
    const { destination, source, draggableId } = result;

    setEnabledIds([0, []]);

    if (!destination) {
      return;
    }

    if (
      destination.droppableId === source.droppableId &&
      destination.index === source.index
    ) {
      return;
    }

    const realDraggableId = getRealId(draggableId);
    const realSourceDroppableId = getRealId(source.droppableId);
    const realDestinationDroppableId = getRealId(destination.droppableId);

    setLoading(true);

    const actionAndLang = getActionAndLang(realDestinationDroppableId, realSourceDroppableId, realDraggableId);

    if (actionAndLang.action.SuppressComment) {
      callChangeDispatch(realDraggableId, actionAndLang.action.ID, actionAndLang.lang, "", destination, source);
    } else {
      setItemToComment({
        destination,
        source,
        draggableId: realDraggableId,
        actionId: actionAndLang.action.ID,
        lang: actionAndLang.lang
      });
      setCommentBoxVisibility(true);
    }
  };

  const callChangeDispatch = (draggableId, actionId, language, comment, destination, source) => {
    dispatch(postChangeWorkflow({
      "ItemId": draggableId,
      "CommandId": actionId,
      "Comment": comment,
      "Language": language,
    })).then(data => {
      if (data.IsSuccess === true) {
        updateBoardStates(destination, source, draggableId);
      }
      dispatch(fetchWorkItems(selectedWorkflowId));
      setLoading(false);
    });
  };

  const updateWorkflowWithComment = (comment, item) => {
    callChangeDispatch(item.draggableId, item.actionId, item.lang, comment, item.destination, item.source);
    setItemToComment({});
  };

  const handleCommentSave = () => {
    const comment = commentTextAreaRef.current && commentTextAreaRef.current.value;
    if (comment.trim() !== "") {
      setCommentErrorMessage("");
      setCommentBoxVisibility(false);
      updateWorkflowWithComment(comment, itemToComment);
    } else {
      setCommentErrorMessage("Please enter a comment!");
    }
  };

  return (
    <section
      className={`board ${isLoading ? "board--is-loading" : ""}`}
    >
      {commentBoxVisible &&
      <div className="item-comment">
        <h4>Comment is required</h4>
        <p>Please add your feedback / comment for this state change!</p>
        <div className="item-comment__form">
          <textarea
            rows={10}
            placeholder="Enter your comment here"
            ref={commentTextAreaRef}
            onKeyPress={(event) => {
              if (event.key.toLowerCase() === "enter") {
                handleCommentSave();
              }
            }}
          >
          </textarea>
          <p className="item-comment__error">{commentErrorMessage}</p>
          <div className="item-comment__buttons">
            <button className="cancel" onClick={() => {
              setCommentErrorMessage("");
              setCommentBoxVisibility(false);
              setItemToComment({});
              setLoading(false);
            }}>Cancel
            </button>
            <button onClick={handleCommentSave}>Save</button>
          </div>
        </div>
      </div>
      }
      <DragDropContext
        onDragEnd={onDragEnd}
        onDragStart={onDragStart}
      >
        <div className="board__content row">
          {
            data && data.States && data.States.map(state => {

              const isDropDisabled = !enabledStateIds[1].includes(state.Id);

              return <Column
                key={state.Id}
                column={state}
                items={state.Items}
                noHighlight={enabledStateIds[0] === state.Id}
                isDropDisabled={isDropDisabled}
              />;
            })
          }
        </div>
      </DragDropContext>
    </section>
  );
};

export default Board;