import Loading from "../../components/Loading";
import { useUploads } from "../../hooks/useUploads";

import "./style.scss";

const UploadsList = () => {
  const { loading, uploads } = useUploads();

  return (
    <Loading loading={loading}>
      <div className="uploads-list">
        {uploads &&
          uploads.map((upload, i) => {
            return (
                <div className="uploads-item">
                    <p key={i}>{JSON.stringify(upload)}</p>
                    <img src={upload} alt={upload}/>
                </div>
            )
          })}
      </div>
    </Loading>
  );
};

export default UploadsList;
