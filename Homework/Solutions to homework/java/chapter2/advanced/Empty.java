package chapter2.advanced;

import java.util.function.Consumer;
import java.util.function.Function;
import java.util.function.Predicate;

public class Empty<T> implements List<T>{

    public int size(){
        return 0;
    }

    public void iter(Consumer<T> f){
    }

    public <U> List<U> map(Function<T,U> f){
        return new Empty<U>();
    }

    public List<T> filter(Predicate<T> p){
        return new Empty<T>();
    }

}
