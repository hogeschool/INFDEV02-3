package chapter2.ListInt;

import java.util.function.Function;
import java.util.function.Predicate;

interface ListInt {
    int size();
    ListInt iterate();
    <R> ListInt map(Function<Integer, Integer> mapper);
    ListInt filter(Predicate<Integer> predicate);
}

class NodeInt implements ListInt{
    private int data;
    private ListInt next;

    public NodeInt(int data, ListInt next) {
        this.data = data;
        this.next = next;
    }

    @Override
    public int size() {
        return 1 + iterate().size();
    }

    @Override
    public ListInt iterate() {
        return next;
    }

    @Override
    public <R> ListInt map(Function<Integer, Integer> function) {
        return new NodeInt(function.apply(data), next.map(function));
    }

    @Override
    public ListInt filter(Predicate<Integer> predicate) {
        if (predicate.test(data)) {
            return new NodeInt(data, next.filter(predicate));
        }else {
            return next.filter(predicate);
        }
    }

    @Override
    public String toString() {
        return data + " > " + next;
    }
}

class EmptyInt implements ListInt{

    @Override
    public int size() {
        return 0;
    }

    @Override
    public ListInt iterate() {
        return this;
    }

    @Override
    public <R> ListInt map(Function<Integer, Integer> mapper) {
        return this;
    }

    @Override
    public ListInt filter(Predicate<Integer> predicate) {
        return this;
    }

    public String toString(){
        return "[]";
    }
}


class Main{
    public static void main(String[] args) {
        ListInt list = new NodeInt(1, new NodeInt(2, new NodeInt(3, new EmptyInt())));
        System.out.println(list);

        ListInt list2 = list.map(integer -> integer * 2);
        System.out.println(list2);

        ListInt list3 = list2.filter(integer -> integer >= 4);
        System.out.println(list3);
    }
}