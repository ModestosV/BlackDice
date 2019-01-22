import React, { Component } from 'react'
import { observable } from 'mobx';
import { observer } from 'mobx-react';

class TermsOfServicePopUp extends React.Component {
	onClose = (e) => {
		this.props.onClose && this.props.onClose(e);
	}
	
	render(){
		if (!this.props.show){
			return null;
		}
		return(
			<div>
				{this.props.children}
				
				<div>
					<button onClick = {(e) => {this.onClose(e)}} >
						Close
					</button>
				</div>
			</div>
		);
	}
}

export default TermsOfServicePopUp;