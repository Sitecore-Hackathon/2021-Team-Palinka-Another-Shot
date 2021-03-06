import React from "react";
import { Droppable } from "react-beautiful-dnd";
import WorkItem from "./WorkItem";

const Column = (props) => {
  return (
    <Droppable droppableId={props.column.id}>
      {(provided, snapshot) => (
        <div className={`col board-column ${snapshot.isDraggingOver ? "board-column--is-dragging-over" : ""}`}>
          <div ref={provided.innerRef} {...provided.droppableProps}>
            {props.tasks.map((task, index) => (
              <WorkItem key={task.id} task={task} index={index}/>
            ))}
            {provided.placeholder}
          </div>
        </div>
      )}
    </Droppable>
  );
};

export default Column;