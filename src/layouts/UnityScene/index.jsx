import Unity, { UnityContext } from "react-unity-webgl";

const UnityScene = ({ currentScene }) => {
  const unityContext = new UnityContext(currentScene.context);

  return (
    <Unity
      unityContext={unityContext}
      style={{
        height: 600,
        width: 950,
        border: "2px solid black",
        background: "grey",
      }}
    />
  );
};

export default UnityScene;
