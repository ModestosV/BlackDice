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
            <div className = "modalPopUp">
                <div className = "modalStyle">
                    {this.props.children}
                    <div className = "modalFooter">
                        <button onClick = {(e) => {this.onClose(e)}} >
                            Close
                        </button>
                        <a>     </a>
                        <button onClick = {(e) => {this.downloadLink(); this.onClose(e);}}>
                            Agree
                        </button>
                    </div>
                </div>
            </div>			
        );
    }
}

export default TermsOfServiceModal;
