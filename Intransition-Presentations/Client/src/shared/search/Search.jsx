import styles from './main.module.css';
import search from './images/search.png';

const Search = ({changeInput = () => {}}) => {
    return (
        <div className={styles.search}>
            <img src={search} />
            <input placeholder='Search' onInput={(event) => {changeInput(event.target.value)}} />
        </div>
    );
}

export default Search;