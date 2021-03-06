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
          <Link className="work-item__title"
                to={`${process.env.PUBLIC_URL}/detail/${props.item.ID}/${props.item.Language}`}>
            {
              props.item.Icon && <img src={props && props.item.Icon} width="16" height="16" alt=""/>
            }
            {props.item.Name}
          </Link>
          <p>Language: {props.item.Language}</p>
          <p>Last updated: <b>{new Date(props.item.LastUpdated).toLocaleString()}</b> by {props.item.LastUpdatedBy}</p>
          <p>Template name: {props.item.TemplateName}</p>
          {
            props.item.CurrentVersion &&
            <p>Current Version: {props.item.CurrentVersion}</p>
          }
        </div>
      )}
    </Draggable>
  );
};

export default WorkItem;