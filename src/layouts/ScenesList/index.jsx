import Loading from "../../components/Loading";
import { useScenes } from "../../hooks/useScenes";
import { toHumanReadable } from "../../utils/string";

import "./style.scss";

const ScenesList = ({ onSceneSelect, currentScene }) => {
  const { loading, scenes } = useScenes();


  return (
    <Loading loading={loading}>
      <div className="scenes-list">
        {scenes &&
          scenes.map((scene, i) => {
            const selected = currentScene && scene.id === currentScene.id;
            return (
              <button onClick={() => onSceneSelect(scene)} key={i}>
                {selected && "*"}
                {toHumanReadable(scene.name)}
              </button>
            );
          })}
      </div>
    </Loading>
  );
};

export default ScenesList;
