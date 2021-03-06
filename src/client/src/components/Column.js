import React from "react";
import { Droppable } from "react-beautiful-dnd";
import WorkItem from "./WorkItem";

const Column = (props) => {
  return (
    <Droppable droppableId={props.column.Id}>
      {(provided, snapshot) => (
        <div className={`col-md col-xs-12 board-column ${snapshot.isDraggingOver ? "board-column--is-dragging-over" : ""}`}>
          <h3>{props.column.Name}</h3>
          <div ref={provided.innerRef} {...provided.droppableProps}>
            {props.items.map((item, index) => (
              <WorkItem key={item.ID} item={item} index={index}/>
            ))}
            {provided.placeholder}
          </div>
        </div>
      )}
    </Droppable>
  );
};

export default Column;