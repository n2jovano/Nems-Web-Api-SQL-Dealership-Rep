import './App.css';
import CustomerCrud from './CustomerCrud';
import VehicleCrud from './VehicleCrud';

function App() {
  return (
    <div className="App">    
      <CustomerCrud />
      <p>------------------------------------------------------</p>
      <VehicleCrud />
    </div>
  );
}

export default App;
