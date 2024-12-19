import React, { useState } from 'react';

const AddConnectionForm = ({ addConnection }) => {
  const [source, setSource] = useState('');
  const [target, setTarget] = useState('');

  const handleSubmit = (e) => {
    e.preventDefault();
    if (source && target) {
      addConnection({ source, target });
      setSource('');
      setTarget('');
    }
  };

  return (
    <form onSubmit={handleSubmit} className="form-container">
      <div>
        <label>Source Node ID:</label>
        <input
          type="text"
          value={source}
          onChange={(e) => setSource(e.target.value)}
        />
      </div>
      <div>
        <label>Target Node ID:</label>
        <input
          type="text"
          value={target}
          onChange={(e) => setTarget(e.target.value)}
        />
      </div>
      <button type="submit">Add Connection</button>
    </form>
  );
};

export default AddConnectionForm;
