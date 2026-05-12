import { createRoot } from 'react-dom/client'
import { Canvas } from '@react-three/fiber'
import { OrbitControls } from "@react-three/drei";

export default function App() {
  return (
    <div id="canvas-container">
		<Canvas>
        	<OrbitControls />			
			<mesh>
				<boxGeometry args={[2, 2, 2]} />
				<meshPhongMaterial />
			</mesh>
			<ambientLight intensity={0.1} />
			<directionalLight position={[0, 0, 5]} color="red" />
		</Canvas>
    </div>
  )
}

//createRoot(document.getElementById('root')).render(<App />)