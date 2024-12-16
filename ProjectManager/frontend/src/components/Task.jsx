// src/components/Task.jsx
import React from 'react';
import { useDrag } from 'react-dnd';

export const Task = ({ task }) => {
  const [{ isDragging }, drag] = useDrag({
    type: 'task',
    item: { id: task.id, status: task.status },
    collect: (monitor) => ({
      isDragging: monitor.isDragging(),
    }),
  });

  return (
    <div
      ref={drag}
      className="task-card"
      style={{
        opacity: isDragging ? 0.5 : 1,
        border: `2px solid ${task.priority.color}`,
        padding: '10px',
        marginBottom: '10px',
      }}
    >
      <h4>{task.title}</h4>
      <p>{task.description}</p>
      <div>
        <span style={{ color: task.priority.color }}>{task.priority.priority}</span>
      </div>
    </div>
  );
};
