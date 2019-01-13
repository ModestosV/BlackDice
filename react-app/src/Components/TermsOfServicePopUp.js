import React, { Component } from 'react'

class TermsOfServicePopUp extends Component {
	render(){
		return
			<
				input type="text"
				placeholder={this.props.placeholder}
				className = "mm-popup)_input"
				value={this.state.value}
				onChange={this.onChange}
			/>;
	}
}

export default TermsOfServicePopUp;