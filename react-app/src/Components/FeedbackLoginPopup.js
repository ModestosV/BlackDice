import React, { Component } from 'react'
import FeedbackLoginModal from "../Components/FeedbackLoginModal"
import '../App.css';

class FeedbackLoginPopup extends Component {
  state = {
    ...this.props.state,
    show:false
  }

  showModal = () => {
      this.setState({
          ...this.state,
          show: !this.state.show
      });
  }

  render() {
    return (
      <div>
        { /* eslint-disable-next-line jsx-a11y/anchor-is-valid */ }
        <strong><a id="link" onClick={this.showModal}>Feedback</a></strong>
        <FeedbackLoginModal show={this.state.show} onClose={this.showModal} state={this.state}></FeedbackLoginModal>
      </div>
    );
  }

}

export default FeedbackLoginPopup;