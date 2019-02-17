import React, { Component } from 'react'
import { InputGroup, Button, Container, Col, Row, Alert } from 'react-bootstrap'
import crypto from 'crypto-js'; 
import ReactDOM from 'react-dom';
import FeedbackView from './FeedbackView'
import '../App.css';

class FeedbackLoginModal extends Component {

  state = this.props.state

  onClose = (e) => {
    this.props.onClose && this.props.onClose(e);
  }

  styleButtonContainer = {
    "width" : "20%",
    "padding-top" : "3%"

  }

  styleContainer = {
    "padding-top":"3%",
    "padding-left" : "40%"
  }

  styleButtons = {
    "margin-right" : "1%",
    "margin-top" : "1%"
  }

  styleRow = {
    "padding-top" : "3%"
  }

  hashInput(pass) {
    return crypto.SHA1(pass).toString();
  }

  async getFeedback() {
    try {

      var username = document.getElementById("Username").value;
      var password = this.hashInput(document.getElementById("Password").value)

      fetch('/account/login',{
        method:'POST',
        headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json',
        },
        body:JSON.stringify({
          username: username,
          password: password
        })
      }).then((r) => r.json())
      .then((token) => {
        

        fetch('/feedback/all/'+token)
        .then((r) => r.json())
        .then(async (feedback) => {

          this.setState({
            feedback: feedback.everything
          })


          await fetch('/account/logout/token', {
            method: 'POST',
            headers: {
              'Accept' : 'application/json',
              'Content-Type': 'application/json',
            },
            body:JSON.stringify({
              token
            })
          })

          ReactDOM.render(
            <FeedbackView feedback={this.state.feedback}/>,
            document.getElementById('root')
          );


        })        
      })
    } catch(err) {
      this.setState({ alerts: [
          <Alert dismissible variant="danger">
            The feeback was unable to be fetched
          </Alert>
        ]
      })
    }
  }

  async responseValue(promise) {
    await promise.then((r) => r.json())
    .then((json) => {
      return json;
    })
  }

  render(){
    
    if (!this.props.show){
      return null;
    }
    
    return (
      <div className="modalPopUp">
        <div className = "modalStyle">
          <h1>Feedback View (For admin use only)</h1>
          <Container style={this.styleContainer}>
            <InputGroup>
              <Row style={this.styleRow}>
                <Col>
                  <label>Username</label>
                </Col>
                <Col>
                  <input id="Username" type="text"/>
                </Col>
              </Row>
              <Row style={this.styleRow}>
                <Col>
                  <label>Password</label>
                </Col>
                <Col>
                  <input id="Password" type="password"/>
                </Col>
              </Row>
            </InputGroup>

            <Button  style = {this.styleButtons} onClick = {(e) => {this.onClose(e);}} >
                Cancel
            </Button>
            <Button style={this.styleButtons} onClick = {(e) => {this.getFeedback(); this.onClose(e);}}>
                Submit
            </Button>
          </Container>
        </div>
      </div>
    );
  }
};

export default FeedbackLoginModal;