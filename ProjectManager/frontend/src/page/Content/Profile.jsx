import React, { useState, useCallback, useMemo } from 'react';
import { TaskBoard, TaskBoardToolbar } from '@progress/kendo-react-taskboard';
import { filterBy } from '@progress/kendo-data-query';
import { Task } from '../../components/Task';
import { Column } from '../../components/Column';
import { cards } from '../../data/cards';
import { DndProvider } from 'react-dnd';
import { HTML5Backend } from 'react-dnd-html5-backend';
import "../../css/Profile.css";

const tasks = cards.map((c) => ({
  id: c.id,
  title: c.title,
  description: c.description,
  status: c.status,
  priority: c.priority,
}));

const columns = [
  { id: 1, title: 'To-Do', status: 'todo' },
  { id: 2, title: 'In Progress', status: 'inProgress' },
  { id: 3, title: 'Done', status: 'done' },
];

const Profile = () => {
  const [filter, setFilter] = useState('');
  const [taskData, setTaskData] = useState(tasks);
  const [columnsData, setColumnsData] = useState(columns);

  const onSearchChange = useCallback((event) => {
    setFilter(event.value);
  }, []);

  const resultTasks = useMemo(() => {
    const filterDesc = {
      logic: 'and',
      filters: [
        {
          field: 'title',
          operator: 'contains',
          value: filter,
        },
      ],
    };
    return filter ? filterBy(taskData, filterDesc) : taskData;
  }, [filter, taskData]);

  const onChangeHandler = useCallback((event) => {
    if (event.type === 'column') {
      setColumnsData(event.data);
    } else {
      setTaskData(event.data);
    }
  }, []);
  const onDrop = (taskId, status) => {
    const updatedTasks = taskData.map((task) =>
      task.id === taskId ? { ...task, status } : task
    );
    setTaskData(updatedTasks);
  };

  return (
    <DndProvider backend={HTML5Backend}>
      <TaskBoard
        columnData={columnsData}
        taskData={resultTasks}
        priorities={columnsData}
        onChange={onChangeHandler}
        column={(props) => (
          <Column {...props} tasks={taskData} onDrop={onDrop} />
        )}
        card={Task}
        style={{ height: '700px' }}
        tabIndex={0}
      >
        <TaskBoardToolbar />
      </TaskBoard>
    </DndProvider>
  );
};

export default Profile;
