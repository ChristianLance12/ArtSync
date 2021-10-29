import React, { useRef } from 'react';
import axios from 'axios';
import { uniqueId } from 'lodash';

const FileUpload = ({afterUpload}) => {

  const inputRef = useRef(null);

  const handleUploadSuccess = (data) => {
    console.log('SUCCESS', data)
    afterUpload && afterUpload(data);
    inputRef.current.value = null;
  }

  const uploadFile = () => {

    if (!inputRef.current) return;

    const file = inputRef.current.files[0];
    if (!file) return console.log("NO FILE")

    const formData = new FormData();
    const headers = { 'Content-Type': 'multipart/form-data' }

    const _file = new File([file], `${uniqueId()}-${file.name}`);

    formData.append("file", _file);
    axios.post('/static_content/upload', formData, {headers})
    .then(handleUploadSuccess)
    .catch(error => console.log('ERROR', error));
  }

  return (
    <div>
      <title>Upload new File</title>
      <h1>Upload new File</h1>
      <input 
        type="file" 
        name="file" 
        ref={inputRef}
      />

      <button onClick={uploadFile}>SUBMIT</button>
    </div>
  );
}

export default FileUpload;