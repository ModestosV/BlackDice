import React, { Component } from 'react'

const modalPopUp = {
	position: 'fixed',
	top: 0,
	bottom: 0,
	left: 0,
	right: 0,
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

class TermsOfServiceModal extends React.Component {
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
						<form action="https://google.ca">
							<input type = "submit" value="Agree"/>
						</form>
					</div>
				</div>
				
				<style>
					
				</style>
			</div>			
		);
	}
}

export default TermsOfServiceModal;