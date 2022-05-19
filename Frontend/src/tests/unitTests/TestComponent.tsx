import React, {useState} from "react";

const TestComponent = () => {
    const [count, setCount] = useState(0);

    const handleIncreaseCount = () => {
        setCount(count+1);
    }

    return(
        <div>
            <span role='span'>{count}</span>
            <button onClick={handleIncreaseCount}>Increase count</button>
        </div>
    )
}

export default TestComponent;