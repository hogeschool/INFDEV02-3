package exercises.exercise2.advanced;

import java.util.function.Consumer;
import java.util.function.Function;
import java.util.function.Predicate;

public class Node<T> implements List<T>{

    T head;
    List<T> tail;

    public Node(T x, List<T> xs){
        head = x;
        tail = xs;
    }

    public int size(){
        return 1 + tail.size();
    }

    public List<T> filter(Predicate<T> p){
        if(p.test(head)){
            return new Node<T>(head, tail.filter(p));
        }else{
            return tail.filter(p);
        }
    }

    public void iter(Consumer<T> f){
        f.accept(head);
        tail.iter(f);
    }

    public <U> List<U> map(Function<T, U> f){
        return new Node(f.apply(head), tail.map(f));
    }

}
