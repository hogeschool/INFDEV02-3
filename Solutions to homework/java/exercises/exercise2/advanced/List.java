package exercises.exercise2.advanced;

import java.util.function.Consumer;
import java.util.function.Function;
import java.util.function.Predicate;

public interface List<T> {
    int size();
    void iter(Consumer<T> f);
    <U> List<U> map(Function<T,U> f);
    List<T> filter(Predicate<T> p);
}
