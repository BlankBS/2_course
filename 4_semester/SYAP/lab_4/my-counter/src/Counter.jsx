    import React, {useState} from "react";
    import Button from "./Button";

    function Counter() {
        const [count, setCount] = useState(0);

        const handleIncrease = () => {
            setCount(count + 1);
        };

        const handleReset = () => {
            setCount(0);
        };

        const isIncreaseDisabled = count >= 5;
        const isResetDisabled = count === 0;

        return (
        <div style={{ padding: '20px', textAlign: 'center' }}>
        <h1>Счетчик: {count}</h1>

        <Button
            title="Increase"
            onClick={handleIncrease}
            disabled={isIncreaseDisabled}
        />

        <Button
            title="Reset"
            onClick={handleReset}
            disabled={isResetDisabled}
        />
        </div>
    );
    }

    export default Counter;