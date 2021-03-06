import React from "react";
import { Droppable } from "react-beautiful-dnd";
import WorkItem from "./WorkItem";

const Column = (props) => {
  return (
    <Droppable droppableId={props.column.id}>
      {(provided, snapshot) => (
        <div className={`col-md col-xs-12 board-column ${snapshot.isDraggingOver ? "board-column--is-dragging-over" : ""}`}>
          <h3>{props.column.title}</h3>
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