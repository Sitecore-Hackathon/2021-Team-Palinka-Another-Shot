import React from "react";
import { Link } from "react-router-dom";
import { Draggable } from "react-beautiful-dnd";

const WorkItem = (props) => {
  return (
    <Draggable draggableId={props.item.ID} index={props.index}>
      {(provided, snapshot) => (
        <div
          className={`work-item ${snapshot.isDragging ? "work-item--is-dragging" : ""}`}
          {...provided.draggableProps}
          {...provided.dragHandleProps}
          ref={provided.innerRef}
        >
          <Link to={`${process.env.PUBLIC_URL}/detail/${props.item.ID}/${props.item.Language}`}>{props.item.Name}</Link>
        </div>
      )}
    </Draggable>
  );
};

export default WorkItem;