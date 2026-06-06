import type { ButtonHTMLAttributes } from "react";

interface ButtonProps
  extends ButtonHTMLAttributes<HTMLButtonElement> {
  children: React.ReactNode;
}

const Button = ({
  children,
  ...props
}: ButtonProps) => {
  return (
    <button
      {...props}
      className="w-full rounded-lg bg-blue-600 px-4 py-2 text-white transition hover:bg-blue-700"
    >
      {children}
    </button>
  );
};

export default Button;