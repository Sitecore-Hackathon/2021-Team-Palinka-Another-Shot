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
            {props.item.Name}
          </Link>
          <p>Language: {props.item.Language}</p>
          <p>Last updated: {new Date(props.item.LastUpdated).toLocaleString()} by {props.item.LastUpdatedBy}</p>
          <p>Template name: {props.item.TemplateName}</p>
          <img src={props && props.item.Icon} width="16" height="16"/>
          <Link to={`${process.env.PUBLIC_URL}/detail/${props.item.ID}/${props.item.Language}`}>{props.item.Name}</Link>
          <br/>
          <strong>Updated by:</strong>{props.item.LastUpdatedBy}<br/>
          <strong>Updated at:</strong>{props.item.LastUpdated}<br/>
          <strong>Current Version: </strong>{props.item.CurrentVersion}
        </div>
      )}
    </Draggable>
  );
};

export default WorkItem;