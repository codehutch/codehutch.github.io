import { Canvas } from '@react-three/fiber'
import { OrbitControls } from "@react-three/drei";

export default function App() {
  return (
    <div id="canvas-container">
		<Canvas>
        	<OrbitControls 
				rotateSpeed={1}
				autoRotate={true}
				autoRotateSpeed={15}			
			/>			
			<mesh position={[-1.25 , -1.25, 0]}><boxGeometry args={[1.25, 1.25, 1.25]} /><meshPhongMaterial /></mesh>
			<mesh position={[-1.25 ,  0   , 0]}><boxGeometry args={[1.25, 1.25, 1.25]} /><meshPhongMaterial /></mesh>
			<mesh position={[-1.25 ,  1.25, 0]}><boxGeometry args={[1.25, 1.25, 1.25]} /><meshPhongMaterial /></mesh>
			<mesh position={[ 0    ,  0   , 0]}><boxGeometry args={[1.25, 1.25, 1.25]} /><meshPhongMaterial /></mesh>
			<mesh position={[ 1.25 , -1.25, 0]}><boxGeometry args={[1.25, 1.25, 1.25]} /><meshPhongMaterial /></mesh>
			<mesh position={[ 1.25 ,  0   , 0]}><boxGeometry args={[1.25, 1.25, 1.25]} /><meshPhongMaterial /></mesh>
			<mesh position={[ 1.25 ,  1.25, 0]}><boxGeometry args={[1.25, 1.25, 1.25]} /><meshPhongMaterial /></mesh>
			<ambientLight intensity={0.1} />
			<directionalLight position={[5, 4, 3]} intensity={0.95} color="cyan" />
			<directionalLight position={[-5, -4, -3]} intensity={0.55} color="magenta" />			
		</Canvas>
    </div>
  )
}

