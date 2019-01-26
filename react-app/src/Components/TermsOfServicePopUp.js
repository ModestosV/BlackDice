import React, { Component } from 'react'
import TermsOfServiceModal from './TermsOfServiceModal'

class TermsOfServicePopUp extends React.Component {
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
		return(
			<li className="DownloadItem">
				<strong><a onClick={this.showModal}> {this.props.download.title} </a></strong>
				<TermsOfServiceModal onClose = {this.showModal} show={this.state.show}>
					Modal Test
				</TermsOfServiceModal>
			</li>
		);		
	}
}

export default TermsOfServicePopUp;