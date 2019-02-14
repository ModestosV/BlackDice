import React, { Component } from 'react'
import '../App.css';

class feedbackLoginPopup extends Component {
  state = {
      show:false
  }

  showModal = () => {
      this.setState({
          ...this.state,
          show: !this.state.show
      });
  }

  render(){
    return (
      <p></p>
    );
  }
}

export default feedbackLoginPopup;