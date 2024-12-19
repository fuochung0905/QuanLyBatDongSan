import React, { useState, useEffect } from 'react';
import ReactFlow, {
  Background,
  Controls,
  MiniMap,
  addEdge,
  useEdgesState,
  useNodesState,
} from 'reactflow';
import 'reactflow/dist/style.css';
import { initialEdges } from '../../../data/edges.js';
import { initialNodes } from '../../../data/nodes.js';


const Workflow = () => {
    const [nodes, setNodes, onNodesChange] = useNodesState(initialNodes);
    const [edges, setEdges, onEdgesChange] = useEdgesState(initialEdges);
    const [contextMenu, setContextMenu] = useState(null); 
    const [editLabel, setEditLabel] = useState(''); 
    const [showDeletePanel, setShowDeletePanel] = useState(false);
  
    const onNodeContextMenu = (event, node) => {
      event.preventDefault(); // Ngăn hành động mặc định
      setContextMenu({
        x: event.clientX,
        y: event.clientY,
        type: 'node', // Menu loại node
        node, // Lưu thông tin node
      });
    };
  
    const onEdgeContextMenu = (event, edge) => {
      event.preventDefault();
      setContextMenu({
        x: event.clientX,
        y: event.clientY,
        type: 'edge', // Menu loại edge
        edge, // Lưu thông tin edge
      });
      setEditLabel(edge.label || ''); // Đặt tên liên kết hiện tại vào trạng thái chỉnh sửa
      setShowPanel(true); // Hiển thị panel chỉnh sửa
      setShowDeletePanel(false); // Ẩn panel xóa khi mở panel chỉnh sửa
    };
  
    // Xóa node và edges liên quan
    const handleDeleteNode = () => {
      if (contextMenu && contextMenu.node) {
        const nodeToDelete = contextMenu.node;
        setNodes((nds) => nds.filter((n) => n.id !== nodeToDelete.id)); // Xóa node
        setEdges((eds) =>
          eds.filter((e) => e.source !== nodeToDelete.id && e.target !== nodeToDelete.id)
        ); // Xóa edges liên quan
        alert(`Deleted node with ID: ${nodeToDelete.id}`);
        setContextMenu(null); // Đóng menu
        setShowDeletePanel(false); // Ẩn panel xóa sau khi thực hiện
      }
    };
  
    // Xóa edge
    const handleDeleteEdge = () => {
      if (contextMenu && contextMenu.edge) {
        const edgeToDelete = contextMenu.edge;
        setEdges((eds) => eds.filter((e) => e.id !== edgeToDelete.id));
        alert(`Deleted connection:\nSource ID: ${edgeToDelete.source}\nTarget ID: ${edgeToDelete.target}`);
        setContextMenu(null); // Đóng menu
        setShowDeletePanel(false); // Ẩn panel xóa sau khi thực hiện
      }
    };
  
    // Cập nhật tên liên kết
    const handleUpdateEdgeLabel = () => {
      if (contextMenu && contextMenu.edge) {
        const edgeToUpdate = contextMenu.edge;
        setEdges((eds) =>
          eds.map((e) =>
            e.id === edgeToUpdate.id
              ? { ...e, label: editLabel } // Cập nhật label
              : e
          )
        );
        alert(`Updated label to: "${editLabel}"`);
        setShowPanel(false); // Ẩn panel sau khi cập nhật
        setContextMenu(null); // Đóng menu
      }
    };
  
    // Hủy chỉnh sửa
    const handleCancelEdit = () => {
      setShowPanel(false); // Ẩn panel khi nhấn hủy
      setContextMenu(null); // Đóng menu
    };
  
    // Hủy xóa
    const handleCancelDelete = () => {
      setShowDeletePanel(false); // Ẩn panel xóa khi nhấn hủy
      setContextMenu(null); // Đóng menu
    };
  
    return (
      <div style={{ height: '80vh', border: '1px solid #ccc', position: 'relative' }}>
        <ReactFlow
          nodes={nodes}
          edges={edges}
          onNodesChange={onNodesChange}
          onEdgesChange={onEdgesChange}
          onNodeContextMenu={onNodeContextMenu} // Menu ngữ cảnh cho node
          onEdgeContextMenu={onEdgeContextMenu} // Menu ngữ cảnh cho edge
          onConnect={(params) => setEdges((eds) => addEdge(params, eds))}
          fitView
        >
          <MiniMap />
          <Controls />
          <Background />
        </ReactFlow>
  
        {/* Menu ngữ cảnh */}
        {contextMenu && (
          <div
            style={{
              position: 'absolute',
              top: contextMenu.y,
              left: contextMenu.x,
              background: '#fff',
              boxShadow: '0px 0px 10px rgba(0,0,0,0.1)',
              padding: '10px',
              borderRadius: '5px',
              zIndex: 1000,
            }}
          >
            {contextMenu.type === 'node' && (
              <>
                <button
                  onClick={() => setShowDeletePanel(true)} // Hiển thị panel xóa khi nhấn vào
                  style={{
                    cursor: 'pointer',
                    display: 'block',
                    marginBottom: '5px',
                  }}
                >
                  Xóa trạng thái
                </button>
              </>
            )}
            {contextMenu.type === 'edge' && (
              <>
                <label style={{ display: 'block', marginBottom: '5px' }}>
                  Tên liên kết:
                  <input
                    type="text"
                    value={editLabel}
                    onChange={(e) => setEditLabel(e.target.value)}
                    style={{ marginLeft: '5px' }}
                  />
                </label>
                <button
                  onClick={handleUpdateEdgeLabel}
                  style={{
                    cursor: 'pointer',
                    display: 'block',
                    marginBottom: '5px',
                  }}
                >
                  Cập nhật tên
                </button>
                <button
                  onClick={handleDeleteEdge}
                  style={{
                    cursor: 'pointer',
                    display: 'block',
                  }}
                >
                  Xóa liên kết
                </button>
              </>
            )}
          </div>
        )}
  
      
        {showDeletePanel && (
          <div
            style={{
              position: 'absolute',
              top: '20%',
              left: '30%',
              background: 'rgba(0,0,0,0.7)',
              padding: '20px',
              borderRadius: '8px',
              zIndex: 2000,
              width: '300px',
              color: 'white',
            }}
          >
            <h3>Chắc chắn bạn muốn xóa?</h3>
            <button
              onClick={handleDeleteNode}
              style={{
                cursor: 'pointer',
                padding: '10px',
                backgroundColor: 'red',
                color: 'white',
                border: 'none',
                marginRight: '10px',
              }}
            >
              Xóa trạng thái
            </button>
         
          <button
            onClick={handleCancelDelete}
            style={{
              cursor: 'pointer',
              padding: '10px',
              backgroundColor: 'gray',
              color: 'white',
              border: 'none',
              marginTop: '10px',
            }}
          >
            Hủy
          </button>
        </div>
      )}
    </div>
  );
};
export default Workflow;
