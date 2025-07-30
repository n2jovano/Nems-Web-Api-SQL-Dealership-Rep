import React, { Fragment, useEffect, useState } from 'react'
import Table from 'react-bootstrap/Table';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
//axios
import axios from "axios";
import {ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css'  

const VehicleCrud = () => {


    const [show, setShow] = useState(false);
    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);


    const [name, setName] = useState("");
    const [vehicleYear, setVehicleYear] = useState("");
    const [colour, setColour] = useState('');
    const [isAutomatic, setIsAutomatic] = useState(true);
    const [orderDate, setOrderDate] = useState("");
    const [customerId, setCustomerId] = useState("");
    const [storeId, setStoreId] = useState("");


    const [editId, setEditId] = useState("");
    const [editName, setEditName] = useState("");
    const [editVehicleYear, setEditVehicleYear] = useState("");
    const [editColour, setEditColour] = useState("");
    const [editIsAutomatic, setEditIsAutomatic] = useState(true);
    const [editOrderDate, setEditOrderDate] = useState("");
    const [editCustomerId, setEditCustomerId] = useState("");
    const [editStoreId, setEditStoreId] = useState("");


    const [data, setData] = useState([]);
    

    useEffect(()=>{
        getData();
    },[])


    const getData = () =>{
        axios.get('https://localhost:7040/api/Vehicle')
        .then((result)=>{
            setData(result.data)
        })
        .catch((error)=>{
            console.log(error)
        })
    }

    const handleEdit = (id) =>{
        handleShow();
        axios.get(`https://localhost:7040/api/Vehicle/${id}`)
        .then((result) => {
            setEditName(result.data.name);
            setEditVehicleYear(result.data.vehicleYear);
            setEditColour(result.data.colour);
            setEditIsAutomatic(result.data.isAutomatic);
            setEditOrderDate(result.data.orderDate);
            setEditCustomerId(result.data.customerId);
            setEditStoreId(result.data.storeId);
            setEditId(id);
        })
        .catch((error) => {
            console.log(error);
        })

    }

    const handleDelete = (id) => {
        if (window.confirm("please confirm you want to delete?") === true) {
            axios.delete(`https://localhost:7040/api/Vehicle/${id}`)
            .then((result) => {
                toast.success('customer deleted');
                getData();
            })
            .catch((error) => {
                console.log(error);
                toast.error(error);
            })
        }  
    }

        const handleUpdate = (id) =>{
        const url = `https://localhost:7040/api/Vehicle/${editId}`
        const data = {
            "id": editId,
            "name": editName,
            "vehicleYear": editVehicleYear,
            "colour": editColour,
            "isAutomatic": editIsAutomatic,
            "orderDate": editOrderDate,
            "customerId": editCustomerId,
            "storeId": editStoreId
        }

        axios.put(url, data)
        .then((result) => {
            handleClose();
            getData();
            clear();
            toast.success("vehicle updated!")
        }).catch((error) => {
            console.log(error);
            toast.error(error);
        })
    }

    const handleSave = (id) => {
        const url = `https://localhost:7040/api/Vehicle?customerId=${customerId}`;
        const data = {
            "name": name,
            "vehicleYear": vehicleYear,
            "colour": colour,
            "isAutomatic": isAutomatic,
            "orderDate": orderDate,
            "storeId": storeId
        }

        axios.post(url, data)
        .then((result)=>{
            getData();
            clear();
            toast.success("customer added!")
        }).catch((error) => {
            console.log(error);
            toast.error(error);
        })
    }

    const clear = () => {
        setName("");
        setVehicleYear("");
        setColour("");
        setIsAutomatic(false);
        setOrderDate("2025-07-29T19:27:09.096Z");
        setCustomerId("");
        setStoreId("");
        setEditId("")
    }

         const handleActiveChange = (e) => {
        if (e.target.checked) {
            setIsAutomatic(true)

        }
        else
            setIsAutomatic(false);
     }

    const handleEditActiveChange = (e) => {
        if (e.target.checked) {
            setEditIsAutomatic(true)

        }
        else
            setEditIsAutomatic(false);
    }


    
    return (
        <Fragment>
            <ToastContainer />
            <div>Vehicle List</div>
            <br></br>
            <Container>
                <Row>
                    <Col><input type="text" className="form-control" placeholder="Name" value={name} onChange={(e)=>setName(e.target.value)}/></Col>
                    <Col><input type="text" className="form-control" placeholder="Year" value={vehicleYear} onChange={(e)=>setVehicleYear(e.target.value)}/></Col>
                    <Col><input type="text" className="form-control" placeholder="Colour" value={colour} onChange={(e)=>setColour(e.target.value)}/></Col>
                    <Col><input type="text" className="form-control" placeholder="OrderDate" value={orderDate} onChange={(e)=>setOrderDate(e.target.value)}/></Col>
                    <Col><input type="text" className="form-control" placeholder="CustomerId" value={customerId} onChange={(e)=>setCustomerId(e.target.value)}/></Col>
                    <Col><input type="text" className="form-control" placeholder="StoreId" value={storeId} onChange={(e)=>setStoreId(e.target.value)}/></Col>
                </Row>
                &nbsp;
                <Row> 
                    <Col>
                        <input type="checkbox" checked={isAutomatic === true ? true : false} value={isAutomatic} onChange={(e)=>handleActiveChange(e)}/>
                        <label>Automatic</label>
                    </Col>
                </Row>
                <br></br>
                <Row>
                    <Col>
                        <button className="btn btn-primary" onClick={() => handleSave()}>Submit</button>
                    </Col>
                </Row>
            </Container>
            <br></br>
            <Table striped bordered hover variant="light">
                <thead>
                    <tr>
                        <th>#</th>
                        <th>Name</th>
                        <th>Year</th>
                        <th>Colour</th>
                        <th>Automatic</th>
                        <th>Order Date</th>
                        <th>CustomerId</th>
                        <th>StoreId</th>
                    </tr>
                </thead>
                <tbody>
                    {data && data.length > 0 ? 
                    data.map((item, index)=>{

                    return(
                    <tr key={index}>

                        <td>{index +1}</td>
                        <td>{item.name}</td>
                        <td>{item.vehicleYear}</td>
                        <td>{item.colour}</td>
                        <td>{(item.isAutomatic) === true ? "Yes" : "No"}</td>
                        <td>{item.orderDate}</td>
                        <td>{item.id}</td> 
                        <td>{item.storeId}</td>
                        <td colSpan={2}>
                            <button className='btn btn-primary' onClick={()=>handleEdit(item.id)}>Edit</button> &nbsp;
                            <button className='btn btn-danger' onClick={()=>handleDelete(item.id)}>Delete</button>
                        </td>
                    </tr>
                    )})  : 'loading...'                     
}
                </tbody>
            </Table>
            {/* //6b snippet of window popup*/}
            <Modal show={show} onHide={handleClose} animation={true}>
                <Modal.Header closeButton>
                    <Modal.Title>Update Vehicle</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Row>
                        <Col><input type="text" className="form-control" placeholder="Name" value={editName} onChange={(e) => setEditName(e.target.value)} /></Col>
                        <Col><input type="text" className="form-control" placeholder="Vehicle Year" value={editVehicleYear} onChange={(e) => setEditVehicleYear(e.target.value)} /></Col>
                        <Col><input type="text" className="form-control" placeholder="Colour" value={editColour} onChange={(e) => setEditColour(e.target.value)} /></Col>
                        <Col><input type="text" className="form-control" placeholder="Automatic" value={editIsAutomatic} onChange={(e) => setEditIsAutomatic(e.target.value)} /></Col>
                        <Col><input type="text" className="form-control" placeholder="Order Date" value={editOrderDate} onChange={(e) => setEditOrderDate(e.target.value)} /></Col>
                        <Col><input type="text" className="form-control" placeholder="Store Id" value={editStoreId} onChange={(e) => setEditStoreId(e.target.value)} /></Col>
                    </Row>
                    &nbsp;
                    <Row>
                        <Col>
                            <input type="checkbox" checked={editIsAutomatic === true ? true : false} value={editIsAutomatic} onChange={(e) => handleEditActiveChange(e)} />
                            <label>Automatic</label>
                        </Col>
                    </Row>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Close
                    </Button>
                    <Button variant="primary" onClick={handleUpdate}>
                        Save Changes
                    </Button>
                </Modal.Footer>
            </Modal>
        </Fragment> 
    )
}

export default VehicleCrud;