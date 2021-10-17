import ScenesList from "../../layouts/ScenesList";
import { useState } from "react";
import UnityScene from "../../layouts/UnityScene";

import "./style.scss";

const Home = () => {
  const [currentScene, setCurrentScene] = useState();

  const handleSceneSelect = (scene) => {
    setCurrentScene(scene);
  };

  return (
    <div id="home">
      <ScenesList
        onSceneSelect={handleSceneSelect}
        currentScene={currentScene}
      />

      {currentScene && <UnityScene currentScene={currentScene} />}
    </div>
  );
};

export default Home;
