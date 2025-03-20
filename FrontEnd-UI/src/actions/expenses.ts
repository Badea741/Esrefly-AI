"use server";

import { formSchema } from "@/app/_components/expense-form";
import { COOKIES_KEYS } from "@/lib/enums";
import axios from "axios";
import { AxiosError } from "axios";
import { cookies } from "next/headers";

import { z } from "zod";

export interface ExpenseItem {
  id: string;
  description: string;
  amount: number;
  category: string | null; // category can be a string or null
  createdDate: string;
}
type ExpensesResponse = {
  data: ExpenseItem[];
  success: boolean;
};

export const fetchExpenses = async (): Promise<ExpensesResponse> => {
  const cookieStore = await cookies();

  try {
    const response = await axios.get(
      `${process.env.NEXT_PUBLIC_BACKEND}/api/Expenses/user/` +
        cookieStore.get(COOKIES_KEYS.ESREFLY_USER_ID)?.value
    );

    return {
      data: response.data,
      success: true,
    };
  } catch (error) {
    const axiosError = error as AxiosError;

    // You can customize the error object structure
    const enhancedError = {
      // message: axiosError.message,
      // status: axiosError.response?.status,
      data: axiosError.response?.data as ExpenseItem[],
      success: false,
    };
    return enhancedError;
  }
};

export const addExpense = async (data: z.infer<typeof formSchema>) => {
  const { amount, title, category } = data;
  try {
    const cookieStore = await cookies();

    const res = await axios.post(
      `${process.env.NEXT_PUBLIC_BACKEND}/api/Expenses`,
      {
        amount: Number(amount),
        description: title,
        category: category,
        userId: cookieStore.get(COOKIES_KEYS.ESREFLY_USER_ID)?.value,
      }
    );
    return res.data;
  } catch (e) {
    throw (e as AxiosError).response?.data || (e as Error).message;
  }
};

export const getExpenseById = async (id: string): Promise<ExpenseItem> => {
  try {
    const response = await axios.get(
      `${process.env.NEXT_PUBLIC_BACKEND}/api/Expenses/${id}`
    );
    return response.data;
  } catch (error) {
    const axiosError = error as AxiosError;
    throw axiosError.response?.data || axiosError.message;
  }
};

export const updateExpense = async (
  id: string,
  data: z.infer<typeof formSchema>
) => {
  const { amount, title, category } = data;

  try {
    const res = await axios.put(
      `${process.env.NEXT_PUBLIC_BACKEND}/api/Expenses/${id}`,
      {
        description: title, // Map 'title' from form to 'description' in the backend
        amount: Number(amount), // Ensure amount is a number
        category: category, // Include category
      }
    );

    return { d: res.data, isSuccess: true };
  } catch (e) {
    const axiosError = e as AxiosError;

    // Throw the error response data or a generic error message
    throw axiosError.response?.data || axiosError.message;
  }
};

export const deleteExpense = async (id: string) => {
  try {
    const res = await axios.delete(
      `${process.env.NEXT_PUBLIC_BACKEND}/api/Expenses/${id}`
    );

    return res.data; // Return the response data if needed
  } catch (e) {
    const axiosError = e as AxiosError;

    // Throw the error response data or a generic error message
    throw axiosError.response?.data || axiosError.message;
  }
};
