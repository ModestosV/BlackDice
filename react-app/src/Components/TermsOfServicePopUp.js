import React, { Component } from 'react'
import { observable } from 'mobx';
import { observer } from 'mobx-react';

class TermsOfServicePopUp extends React.Component {
	render(){
		if (!this.props.show){
			return null;
		}
		return(
			<div>
				{this.props.children}
			</div>
		);
	}
}

export default TermsOfServicePopUp;