import React, { Component } from 'react'
import DownloadItem from './DownloadItem'

class Downloads extends Component {

  render() {
    
    let downloadItems

    if (this.props.downloads) {
      downloadItems = this.props.downloads.map(download => {
        return (
          <DownloadItem key={download.title} download={download} />
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
