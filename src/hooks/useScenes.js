import { get } from "axios"
import { useEffect, useState } from "react"

export const useScenes = () => {

  const [scenes, setScenes] = useState();
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    setLoading(true)
    get('/scenes').then( ({data}) => {
      setScenes(data.scenes);
      setLoading(false);
    })
  }, [])

  return { scenes, loading }

}