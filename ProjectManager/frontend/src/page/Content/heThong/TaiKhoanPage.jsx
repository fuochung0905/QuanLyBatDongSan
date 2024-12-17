import React, { useState } from 'react';
import { DataGrid } from '@mui/x-data-grid';
import { Dialog, DialogActions, DialogContent, DialogTitle, Button, TextField } from '@mui/material';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import DeleteIcon from '@mui/icons-material/Delete'
import EditIcon from '@mui/icons-material/Edit';
import { IconButton } from '@mui/material';
function TaiKhoanPage() {
    const [open, setOpen] = useState(false);
    const [openInsert , setOpenInsert] = useState(false);
    const [openDeleteConfirm, setOpenDeleteConfirm] = useState(false); 
    const [rowData, setRowData] = useState(null); 
    const [newData, setNewData] = useState({ name: '', email: '' });
  
    const columns = [
      { field: 'id', headerName: 'ID', width: 150 },
      { field: 'username', headerName: 'Tên tài khoản', width: 200 },
      { field: 'email', headerName: 'Email', width: 250 },
      { field: 'password', headerName: 'Mật khẩu', width: 250 },
      { field: 'vaitro', headerName: 'Vai trò', width: 250 },
      { field: 'lop', headerName: 'Lớp', width: 250 },
      {
        field: 'action',
        headerName: 'Action',
        width: 150,
        renderCell: (params) => (
            <>
            <IconButton
              color="primary"
              onClick={() => handleEditClick(params.row)}
              sx={{ marginRight: 1,
                display: 'inline-block',
                width: 'auto'
              }}
            >
              <EditIcon />
            </IconButton>
            <IconButton
              color="secondary"
              onClick={() => handleDeleteClick(params.row)}
              sx={{ 
                display: 'inline-block',
                width: 'auto'
              }}
            >
              <DeleteIcon />
            </IconButton>
          </>
        ),
      },
    ];
  
    const rows = [
      { id: 1, username: 'Nguyễn Phước Hùng', email: 'john@example.com', password: 'ahihi@dongoc', vaitro: 'DEV FULLSTACK', lop: 'CNTTK62' },
      { id: 2, username: 'Nguyễn Thị Thanh Như', email: 'john@example.com', password: 'ahihi@dongoc', vaitro: 'DEV FULLSTACK', lop: 'CNTTK62' },
      { id: 3, username: 'Vũ Nguyễn Hoàng Bảo', email: 'john@example.com', password: 'ahihi@dongoc', vaitro: 'DEV FE', lop: 'CNTTK62' },
      { id: 4, username: 'Đỗ Viết Tuế', email: 'john@example.com', password: 'ahihi@dongoc', vaitro: 'DEV BE', lop: 'CNTTK62' },
      { id: 5, username: 'Thành Ngọc Huy', email: 'john@example.com', password: 'ahihi@dongoc', vaitro: 'DEV BE', lop: 'CNTTK62' },
    ];
    const handleInsertClick = () =>{
        setOpenInsert(true);
    }
    const handleEditClick = (row) => {
      setRowData(row);
      setNewData({ name: row.name, email: row.email }); 
      setOpen(true);
    };
  
    const handleSave = () => {
      const updatedRows = rows.map((row) =>
        row.id === rowData.id ? { ...row, name: newData.name, email: newData.email } : row
      );
      toast.success('Dữ liệu đã được cập nhật!');
      setOpen(false); 
    };
  
    const handleClose = () => {
      setOpen(false); 
    };
    const handleCloseInsert = ()=>{
        setOpenInsert(false)
    }
    const handleDeleteClick = (row) => {
        setRowData(row); 
        setOpenDeleteConfirm(true); 
      }
  
      const handleDelete = () => {
        const updatedRows = rows.filter(row => row.id !== rowData.id);
        toast.success('Dữ liệu đã bị xóa!');
        setOpenDeleteConfirm(false); 
      }
  
      const handleCloseDeleteConfirm = () => {
        setOpenDeleteConfirm(false); 
      }
    return (
      <div style={{ height: 400, width: '100%' }}>
        <Dialog
        open={openInsert}
        onClose={handleCloseInsert}
        sx={{
            '& .MuiDialog-paper': {
            width: '600px',  
            height: 'auto',  
            maxWidth: '90%', 
            },
        }}
        >
            <DialogTitle>Thêm dữ liệu</DialogTitle>
            <DialogContent>
                <div className="row form-group" style={{ marginBottom: '16px' }}>
                    <div className="col-md-12">
                        <TextField
                        label="UserName"
                        value={newData.email}
                        onChange={(e) => setNewData({ ...newData, email: e.target.value })}
                        fullWidth
                        required
                        />
                    </div>
                </div>
                <div className="row form-group" style={{ marginBottom: '16px' }}>
                    <div className="col-md-12">
                        <TextField
                        label="Email"
                        value={newData.name}
                        onChange={(e) => setNewData({ ...newData, name: e.target.value })}
                        fullWidth
                        required
                        />
                    </div>
                </div>
                <div className="row form-group" style={{ marginBottom: '16px' }}>
                    <div className="col-md-12">
                        <TextField
                        label="Password"
                        value={newData.name}
                        onChange={(e) => setNewData({ ...newData, name: e.target.value })}
                        fullWidth
                        required
                        />
                    </div>
                </div>
                <div className="row form-group" style={{ marginBottom: '16px' }}>
                    <div className="col-md-12">
                        <TextField
                        label="Vai trò"
                        value={newData.name}
                        onChange={(e) => setNewData({ ...newData, name: e.target.value })}
                        fullWidth
                        required
                        />
                    </div>
                </div>
                <div className="row form-group" style={{ marginBottom: '16px' }}>
                    <div className="col-md-12">
                        <TextField
                        label="Lớp"
                        value={newData.name}
                        onChange={(e) => setNewData({ ...newData, name: e.target.value })}
                        fullWidth
                        required
                        />
                    </div>
                </div>
            </DialogContent>
            <DialogActions>
                <Button onClick={handleCloseInsert} color="secondary">
                Hủy
                </Button>
                <Button onClick={handleSave} color="primary">
                Lưu
                </Button>
            </DialogActions>
        </Dialog>

        <Dialog open={openDeleteConfirm} onClose={handleCloseDeleteConfirm}>
          <DialogTitle>Xác nhận xóa</DialogTitle>
          <DialogContent>
            Bạn có chắc chắn muốn xóa dữ liệu này?
          </DialogContent>
          <DialogActions>
            <Button onClick={handleCloseDeleteConfirm} color="secondary">
              Hủy
            </Button>
            <Button onClick={handleDelete} color="primary">
              Xóa
            </Button>
          </DialogActions>
        </Dialog>

        <Button
        variant="outlined"
        color="primary"
        onClick={() => handleInsertClick()}
        sx={{
            fontSize: '16px',      
            padding: '8px 16px',    
            display: 'inline-block',
            width: 'auto', 
            marginLeftr : '10px',   
            marginBottom: '10px'   
        }}
        >
        Thêm
        </Button>
        <DataGrid rows={rows} columns={columns} pageSize={5} />
        
        <Dialog open={open} onClose={handleClose}>
          <DialogTitle>Chỉnh sửa Dữ liệu</DialogTitle>
          <DialogContent>
            <TextField
              label="Name"
              variant="outlined"
              fullWidth
              margin="normal"
              value={newData.name}
              onChange={(e) => setNewData({ ...newData, name: e.target.value })}
            />
            <TextField
              label="Email"
              variant="outlined"
              fullWidth
              margin="normal"
              value={newData.email}
              onChange={(e) => setNewData({ ...newData, email: e.target.value })}
            />
          </DialogContent>
          <DialogActions>
            <Button onClick={handleClose} color="secondary">
              Hủy
            </Button>
            <Button onClick={handleSave} color="primary">
              Lưu
            </Button>
          </DialogActions>
        </Dialog>
      </div>
    );
  };
  
  
  export default TaiKhoanPage;
  