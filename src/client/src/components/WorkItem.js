import React from "react";
import { Draggable } from "react-beautiful-dnd";

const WorkItem = (props) => {
  return (
    <Draggable draggableId={props.item.ID} index={props.index}>
      {(provided, snapshot) => (
        <div
          className={`work-item ${snapshot.isDragging ? 'work-item--is-dragging' : ''}`}
          {...provided.draggableProps}
          {...provided.dragHandleProps}
          ref={provided.innerRef}
        >
          {props.item.Name}
        </div>
      )}
    </Draggable>
  );
};

export default WorkItem;