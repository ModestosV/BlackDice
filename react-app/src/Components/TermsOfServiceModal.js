import React, { Component } from 'react'
import '../App.css';

class TermsOfServiceModal extends React.Component {
	
    onClose = (e) => {
        this.props.onClose && this.props.onClose(e);
    }
	
    downloadLink = () => {
        window.location= this.props.download.link;
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
                        <button onClick = {(e) => {this.downloadLink(); this.onClose(e);}}>
                            Agree
                        </button>
                    </div>
                </div>
				
                <style>
										
                </style>
            </div>			
        );
    }
}

export default TermsOfServiceModal;
