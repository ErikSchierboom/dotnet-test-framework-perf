public class ListOpsTests
{
    [Test]
    public async Task Append_entries_to_a_list_and_return_the_new_list_empty_lists()
    {
        var list1 = new List<int>();
        var list2 = new List<int>();
        await Assert.That(ListOps.Append(list1, list2)).IsEmpty();
    }

    [Test]
    public async Task Append_entries_to_a_list_and_return_the_new_list_list_to_empty_list()
    {
        var list1 = new List<int>();
        var list2 = new List<int> { 1, 2, 3, 4 };
        var expected = new List<int> { 1, 2, 3, 4 };
        await Assert.That(ListOps.Append(list1, list2)).IsEquivalentTo(expected);
    }

    [Test]
    public async Task Append_entries_to_a_list_and_return_the_new_list_empty_list_to_list()
    {
        var list1 = new List<int> { 1, 2, 3, 4 };
        var list2 = new List<int>();
        var expected = new List<int> { 1, 2, 3, 4 };
        await Assert.That(ListOps.Append(list1, list2)).IsEquivalentTo(expected);
    }

    [Test]
    public async Task Append_entries_to_a_list_and_return_the_new_list_non_empty_lists()
    {
        var list1 = new List<int> { 1, 2 };
        var list2 = new List<int> { 2, 3, 4, 5 };
        var expected = new List<int> { 1, 2, 2, 3, 4, 5 };
        await Assert.That(ListOps.Append(list1, list2)).IsEquivalentTo(expected);
    }

    [Test]
    public async Task Concatenate_a_list_of_lists_empty_list()
    {
        var lists = new List<List<int>>();
        await Assert.That(ListOps.Concat(lists)).IsEmpty();
    }

    [Test]
    public async Task Concatenate_a_list_of_lists_list_of_lists()
    {
        var lists = new List<List<int>> { new() { 1, 2 }, new() { 3 }, new(), new() { 4, 5, 6 } };
        var expected = new List<int> { 1, 2, 3, 4, 5, 6 };
        await Assert.That(ListOps.Concat(lists)).IsEquivalentTo(expected);
    }

    [Test]
    public async Task Concatenate_a_list_of_lists_list_of_nested_lists()
    {
        var lists = new List<List<List<int>>> { new() { new List<int> { 1 }, new List<int> { 2 } }, new() { new List<int> { 3 } }, new() { new List<int>() }, new() { new List<int> { 4, 5, 6 } } };
        var expected = new List<List<int>> { new() { 1 }, new() { 2 }, new() { 3 }, new(), new() { 4, 5, 6 } };
        await Assert.That(ListOps.Concat(lists)).IsEquivalentTo(expected);
    }

    [Test]
    public async Task Filter_list_returning_only_values_that_satisfy_the_filter_function_empty_list()
    {
        var list = new List<int>();
        var function = new Func<int, bool>((x) => x % 2 == 1);
        await Assert.That(ListOps.Filter(list, function)).IsEmpty();
    }

    [Test]
    public async Task Filter_list_returning_only_values_that_satisfy_the_filter_function_non_empty_list()
    {
        var list = new List<int> { 1, 2, 3, 5 };
        var function = new Func<int, bool>((x) => x % 2 == 1);
        var expected = new List<int> { 1, 3, 5 };
        await Assert.That(ListOps.Filter(list, function)).IsEquivalentTo(expected);
    }

    [Test]
    public async Task Returns_the_length_of_a_list_empty_list()
    {
        var list = new List<int>();
        await Assert.That(ListOps.Length(list)).IsEqualTo(0);
    }

    [Test]
    public async Task Returns_the_length_of_a_list_non_empty_list()
    {
        var list = new List<int> { 1, 2, 3, 4 };
        await Assert.That(ListOps.Length(list)).IsEqualTo(4);
    }

    [Test]
    public async Task Return_a_list_of_elements_whose_values_equal_the_list_value_transformed_by_the_mapping_function_empty_list()
    {
        var list = new List<int>();
        var function = new Func<int, int>((x) => x + 1);
        await Assert.That(ListOps.Map(list, function)).IsEmpty();
    }

    [Test]
    public async Task Return_a_list_of_elements_whose_values_equal_the_list_value_transformed_by_the_mapping_function_non_empty_list()
    {
        var list = new List<int> { 1, 3, 5, 7 };
        var function = new Func<int, int>((x) => x + 1);
        var expected = new List<int> { 2, 4, 6, 8 };
        await Assert.That(ListOps.Map(list, function)).IsEquivalentTo(expected);
    }

    [Test]
    public async Task Folds_reduces_the_given_list_from_the_left_with_a_function_direction_dependent_function_applied_to_non_empty_list()
    {
        var list = new List<int> { 2, 5 };
        var initial = 5;
        var function = new Func<int, int, int>((x, y) => x / y);
        await Assert.That(ListOps.Foldl(list, initial, function)).IsEqualTo(0);
    }

    [Test]
    public async Task Folds_reduces_the_given_list_from_the_left_with_a_function_empty_list()
    {
        var list = new List<int>();
        var initial = 2;
        var function = new Func<int, int, int>((acc, el) => el * acc);
        await Assert.That(ListOps.Foldl(list, initial, function)).IsEqualTo(2);
    }

    [Test]
    public async Task Folds_reduces_the_given_list_from_the_left_with_a_function_direction_independent_function_applied_to_non_empty_list()
    {
        var list = new List<int> { 1, 2, 3, 4 };
        var initial = 5;
        var function = new Func<int, int, int>((acc, el) => el + acc);
        await Assert.That(ListOps.Foldl(list, initial, function)).IsEqualTo(15);
    }

    [Test]
    public async Task Folds_reduces_the_given_list_from_the_right_with_a_function_direction_dependent_function_applied_to_non_empty_list()
    {
        var list = new List<int> { 2, 5 };
        var initial = 5;
        var function = new Func<int, int, int>((x, y) => x / y);
        await Assert.That(ListOps.Foldr(list, initial, function)).IsEqualTo(2);
    }

    [Test]
    public async Task Folds_reduces_the_given_list_from_the_right_with_a_function_empty_list()
    {
        var list = new List<int>();
        var initial = 2;
        var function = new Func<int, int, int>((acc, el) => el * acc);
        await Assert.That(ListOps.Foldr(list, initial, function)).IsEqualTo(2);
    }

    [Test]
    public async Task Folds_reduces_the_given_list_from_the_right_with_a_function_direction_independent_function_applied_to_non_empty_list()
    {
        var list = new List<int> { 1, 2, 3, 4 };
        var initial = 5;
        var function = new Func<int, int, int>((acc, el) => el + acc);
        await Assert.That(ListOps.Foldr(list, initial, function)).IsEqualTo(15);
    }

    [Test]
    public async Task Reverse_the_elements_of_the_list_empty_list()
    {
        var list = new List<int>();
        await Assert.That(ListOps.Reverse(list)).IsEmpty();
    }

    [Test]
    public async Task Reverse_the_elements_of_the_list_non_empty_list()
    {
        var list = new List<int> { 1, 3, 5, 7 };
        var expected = new List<int> { 7, 5, 3, 1 };
        await Assert.That(ListOps.Reverse(list)).IsEquivalentTo(expected);
    }

    [Test]
    public async Task Reverse_the_elements_of_the_list_list_of_lists_is_not_flattened()
    {
        var list = new List<List<int>> { new() { 1, 2 }, new() { 3 }, new(), new() { 4, 5, 6 } };
        var expected = new List<List<int>> { new() { 4, 5, 6 }, new(), new() { 3 }, new() { 1, 2 } };
        await Assert.That(ListOps.Reverse(list)).IsEquivalentTo(expected);
    }
}