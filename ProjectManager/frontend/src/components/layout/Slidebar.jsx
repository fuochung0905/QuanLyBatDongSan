import React from 'react';
const Slidebar = () =>{
    return (<div className="col-md bg-light p-3" style={{height: '100vh'}}>
        <h4>Slide bar</h4>
        <ul>
            <li>
                <a href="#">Dashboard</a>
            </li>
            <li>
                <a href="#">Setting</a>
            </li>
            <li>
                <a href="#">Profile</a>
            </li>
        </ul>
    </div>
    );
};
export default Slidebar;