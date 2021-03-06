import React, { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { workItemsSelector } from "../redux/slices/workitems";
import { DragDropContext } from "react-beautiful-dnd";
import Column from "./Column";

const Board = () => {
  const [data, setData] = useState(null);
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

  const onDragEnd = result => {
    const { destination, source, draggableId } = result;

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

      const newItems = reOrderItems(start.Items, newItemIds);
      const newColumn = {
        ...start,
        Items: newItems,
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
    const startTaskIds = Array.from(start.taskIds);
    startTaskIds.splice(source.index, 1);
    const newStart = {
      ...start,
      taskIds: startTaskIds,
    };

    const finishTaskIds = Array.from(finish.taskIds);
    finishTaskIds.splice(destination.index, 0, draggableId);
    const newFinish = {
      ...finish,
      taskIds: finishTaskIds,
    };

    const newState = {
      ...data,
      columns: {
        ...data.columns,
        [newStart.id]: newStart,
        [newFinish.id]: newFinish,
      },
    };
    setData(newState);
  };

  return (
    <section className="board">
      <header className="board__header">Board header</header>
      <DragDropContext onDragEnd={onDragEnd}>
        <div className="board__content row">
          {
            data && data.States && data.States.map(state => {
              return <Column key={state.Id} column={state} items={state.Items}/>;
            })
          }
        </div>
      </DragDropContext>
    </section>
  );
};

export default Board;