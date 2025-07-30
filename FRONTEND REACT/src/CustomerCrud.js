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

const CustomerCrud = () => {


    const [show, setShow] = useState(false);

    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);


    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [phone, setPhone] = useState('');

    const [editId, setEditId] = useState();
    const [editFirstName, setEditFirstName] = useState();
    const [editLastName, setEditLastName] = useState();
    const [editPhone, setEditPhone] = useState();


    const [data, setData] = useState([]);
    


    useEffect(()=>{
        // setData(customerData);
        getData();
    },[])

    
    const getData = () =>{
        axios.get('https://localhost:7040/api/Customer')
        .then((result)=>{
            setData(result.data)
        })
        .catch((error)=>{
            console.log(error)
        })
    }


    const handleEdit = (id) =>{
        //alert(id);
        handleShow();
        axios.get(`https://localhost:7040/api/Customer/${id}`)
        .then((result) => {
            setEditFirstName(result.data.firstName);
            setEditLastName(result.data.lastName);
            setEditPhone(result.data.phone);
            setEditId(id);
        })
        .catch((error) => {
            console.log(error);
        })

    }

    const handleDelete = (id) => {
        if (window.confirm("please confirm you want to delete?") === true) {
            axios.delete(`https://localhost:7040/api/Customer/${id}`)
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
        const url = `https://localhost:7040/api/Customer/${editId}`
        const data = {
            "id": editId,
            "firstName": editFirstName,
            "lastName": editLastName,
            "phone": editPhone
        }

        axios.put(url, data)
        .then((result) => {
            handleClose();
            getData();
            clear();
            toast.success("customer updated!")
        }).catch((error) => {
            console.log(error);
            toast.error(error);
        })
    }

    const handleSave = () => {
        const url = 'https://localhost:7040/api/Customer';
        const data = {
            "firstName": firstName,
            "lastName": lastName,
            "phone": phone
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
        setFirstName('');
        setLastName('');
        setPhone('');
        setEditFirstName('');
        setEditLastName('');
        setEditPhone('');
        setEditId('');
    }
    
    return (
        <Fragment>
            <ToastContainer />
            <div>Customer List</div>
            <br></br>
            <Container>
                <Row>
                    <Col><input type="text" className="form-control" placeholder="Enter Full Name" value={firstName} onChange={(e)=>setFirstName(e.target.value)}/></Col>
                    <Col><input type="text" className="form-control" placeholder="Enter Last Name" value={lastName} onChange={(e)=>setLastName(e.target.value)}/></Col>
                    <Col><input type="text" className="form-control" placeholder="Enter Phone" value={phone} onChange={(e)=>setPhone(e.target.value)}/></Col>
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
                        <th>First Name</th>
                        <th>Last Name</th>
                        <th>Phone Number</th>
                    </tr>
                </thead>
                <tbody>
                    {data && data.length > 0 ? 
                    data.map((item, index)=>{

                    return(
                    <tr key={index}>

                        <td>{index +1}</td>
                        <td>{item.firstName}</td>
                        <td>{item.lastName}</td>
                        <td>{item.phone}</td>
                        <td colSpan={3}>
                            <button className='btn btn-primary' onClick={()=>handleEdit(item.id)}>Edit</button> &nbsp;
                            <button className='btn btn-danger' onClick={()=>handleDelete(item.id)}>Delete</button>
                        </td>
                    </tr>
                    )})  : 'loading...'                     
}
                </tbody>
            </Table>
            <Modal show={show} onHide={handleClose} animation={true}>
                <Modal.Header closeButton>
                    <Modal.Title>Update Customer</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Row>
                        <Col><input type="text" className="form-control" placeholder="First Name" value={editFirstName} onChange={(e) => setEditFirstName(e.target.value)} /></Col>
                        <Col><input type="text" className="form-control" placeholder="Last Name" value={editLastName} onChange={(e) => setEditLastName(e.target.value)} /></Col>
                        <Col><input type="text" className="form-control" placeholder="Phone" value={editPhone} onChange={(e) => setEditPhone(e.target.value)} /></Col>
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

export default CustomerCrud;