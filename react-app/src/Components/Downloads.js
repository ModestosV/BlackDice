import React, { Component } from 'react'
import DownloadItem from './DownloadItem'
import TermsOfServicePopUp from './TermsOfServicePopUp'

class Downloads extends Component {

  render() {
    
    let downloadItems

    if (this.props.downloads) {
      downloadItems = this.props.downloads.map(download => {
        return (
          <TermsOfServicePopUp key={download.title} download={download} />
        )
      })
    }

    return (
      <div className="Downloads">
        {downloadItems}
      </div>
    );
  }

}

export default Downloads;
