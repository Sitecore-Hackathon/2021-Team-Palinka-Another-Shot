import React from "react";

const Board = () => {
  return (
    <section className="board">
      <header className="board__header">Board header</header>
      <div className="board__content row">
        <div className="col"><div className="work-item">Item</div></div>
        <div className="col"><div className="work-item">Item</div></div>
        <div className="col"><div className="work-item">Item</div></div>
        <div className="col"><div className="work-item">Item</div></div>
        <div className="col"><div className="work-item">Item</div></div>
        <div className="col"><div className="work-item">Item</div></div>
      </div>
    </section>
  );
};

export default Board;