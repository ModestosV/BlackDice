import React, { Component } from 'react'
import { observable } from 'mobx';
import { observer } from 'mobx-react';

const modalPopUp = {
	position: 'fixed',
	top: 0,
	bottom: 0,
	left: 0,
	right: 0,
	backgroundColor: 'rgba (0, 160, 160)',
	padding: 50,
};

const modalStyle = {
		backgroundColor: 'grey',
		border: 5,
		maxWidth: 500,
		minHeight: 300,
		margin: '0 auto',
		padding: 30,
		position: 'relative',
};

const modalFooter = {
		position: 'absolute',
		bottom: 20,
};

class TermsOfServicePopUp extends React.Component {
	onClose = (e) => {
		this.props.onClose && this.props.onClose(e);
	}
	
	render(){
		if (!this.props.show){
			return null;
		}
		return(
			<div style = {modalPopUp}>
				<div style = {modalStyle}>
					{this.props.children}
					<div style = {modalFooter}>
						<button onClick = {(e) => {this.onClose(e)}} >
							Close
						</button>
					</div>
				</div>
			</div>
		);
	}
}

export default TermsOfServicePopUp;