import React, { useState } from 'react';
import Workflow from '../duAn/Workflow.jsx';

import { initialEdges } from '../../../data/edges.js';



const DA_Workflow = () =>{
    const [edges, setEdges] = useState(initialEdges);
    const addConnection = (connection) => {
      setEdges((prevEdges) => [
        ...prevEdges,
        { id: `e${connection.source}-${connection.target}`, ...connection }
      ]);
    };
  
    return (
      <div>
        <h1>Workflow Management</h1>
        <Workflow edges={edges} />
     
      </div>
    );
  };
export default DA_Workflow;