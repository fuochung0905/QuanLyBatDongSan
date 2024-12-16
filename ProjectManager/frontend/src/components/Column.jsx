import React from 'react';
import { useDrop } from 'react-dnd';
import { Task } from './Task';

export const Column = ({ column, tasks, onDrop }) => {
  const [{ canDrop, isOver }, drop] = useDrop({
    accept: 'task', // Chỉ cho phép kéo thả task vào cột
    drop: (item) => onDrop(item.id, column.status), // Gọi onDrop khi thả
    collect: (monitor) => ({
      canDrop: monitor.canDrop(),
      isOver: monitor.isOver(),
    }),
  });

  const columnTasks = tasks.filter((task) => task.status === column.status);

  return (
    <div
      ref={drop}
      className="column"
      style={{
        width: '300px',
        margin: '0 10px',
        padding: '10px',
        border: '1px solid #ddd',
        backgroundColor: isOver ? 'lightgreen' : 'white',
      }}
    >
      <h3>{column.title}</h3>
      <div>
        {columnTasks.length === 0 ? (
          <p>No tasks available</p>
        ) : (
          columnTasks.map((task) => <Task key={task.id} task={task} />)
        )}
      </div>
    </div>
  );
};
