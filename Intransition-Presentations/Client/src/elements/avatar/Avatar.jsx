import styles from './main.module.css';

const Avatar = ({name, color = 0, size='47px', font='26px'}) => {
    return (
        <div 
            className={styles.avatar} 
            color={`color-${color}`}
            style={{'--size': size, '--font': font}}
        >
            {(name ?? "?")[0].toUpperCase()}
        </div>
    );
}

export default Avatar;