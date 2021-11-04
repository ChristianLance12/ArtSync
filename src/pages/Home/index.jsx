import ScenesList from "../../layouts/ScenesList";
import UnityScene from "../../layouts/UnityScene";
import UnityControls from "../../layouts/UnityControls";
import FileUpload from "../../components/FileUpload";

import { useMemo, useState } from "react";
import { isEmpty } from "lodash";

import "./style.scss";
import UploadsList from "../../layouts/UploadsList";
import { useUploads } from "../../hooks/useUploads";

const Home = () => {
  const [currentScene, setCurrentScene] = useState();
  const [currentUnityContext, setCurrentUnityContext] = useState();

  const { refetchUploadsEvent } = useUploads();

  const handleSceneSelect = (scene) => {
    setCurrentScene(scene);
  };

  const showScene = useMemo(() => !isEmpty(currentScene), [currentScene]);

  // show the unity scene and controls
  if (showScene) {
    return (
      <div id="home">
        <UnityScene
          currentScene={currentScene}
          onLoad={setCurrentUnityContext}
        />

        <UnityControls 
          currentUnityContext={currentUnityContext} 
          setCurrentUnityContext={setCurrentUnityContext}
          currentScene={currentScene}
          setCurrentScene={setCurrentScene}
        />
      </div>
    )
  }
  
  // show the scenes list to choose a scene
  else {
    return (
      <div id="home">
        <ScenesList
          onSceneSelect={handleSceneSelect}
          currentScene={currentScene}
        />

        <hr />

        <FileUpload afterUpload={() => window.dispatchEvent(refetchUploadsEvent)}/>

        <UploadsList/>
      </div>
    );
  }
};

export default Home;
