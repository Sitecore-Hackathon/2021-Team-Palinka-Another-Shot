import React, { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { workItemsSelector } from "../redux/slices/workitems";
import { DragDropContext } from "react-beautiful-dnd";
import Column from "./Column";

const Board = () => {
  const [data, setData] = useState(null);
  const [enabledStateIds, setEnabledIds] = useState([0, []]);
  const { workItems } = useSelector(workItemsSelector);

  useEffect(() => {
    if (workItems && workItems.States) {
      setData(workItems);
    }
  }, [workItems]);

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

  const onDragStart = start => {
    setEnabledIds(
      [
        start.source.droppableId,
        getNextStateIds(start.draggableId, start.source.droppableId)
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

  return (
    <section className="board">
      <header className="board__header">Board header</header>
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