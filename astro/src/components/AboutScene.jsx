import { createRoot } from 'react-dom/client'
import { Canvas } from '@react-three/fiber'
import { OrbitControls } from "@react-three/drei";

export default function App() {
  return (
    <div id="canvas-container">
		<Canvas>
        	<OrbitControls 
				rotateSpeed={2}
				autoRotate={true}
				autoRotateSpeed={5}			
			/>			
			<mesh position={[-2  , -2, 0]}><boxGeometry args={[2, 2, 2]} /><meshPhongMaterial /></mesh>
			<mesh position={[-2  , 0, 0]}><boxGeometry args={[2, 2, 2]} /><meshPhongMaterial /></mesh>
			<mesh position={[-2  , 2, 0]}><boxGeometry args={[2, 2, 2]} /><meshPhongMaterial /></mesh>
			<mesh position={[0   , 0, 0]}><boxGeometry args={[2, 2, 2]} /><meshPhongMaterial /></mesh>
			<mesh position={[2   , -2, 0]}><boxGeometry args={[2, 2, 2]} /><meshPhongMaterial /></mesh>
			<mesh position={[2   , 0, 0]}><boxGeometry args={[2, 2, 2]} /><meshPhongMaterial /></mesh>
			<mesh position={[2   , 2, 0]}><boxGeometry args={[2, 2, 2]} /><meshPhongMaterial /></mesh>
			<ambientLight intensity={0.1} />
			<directionalLight position={[5, 4, 3]} intensity={0.95} color="cyan" />
		</Canvas>
    </div>
  )
}

