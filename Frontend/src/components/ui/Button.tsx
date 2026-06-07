import type { ButtonHTMLAttributes, ReactNode } from "react";

interface ButtonProps
  extends ButtonHTMLAttributes<HTMLButtonElement> {
  children: ReactNode;
  variant?: "primary" | "danger" | "secondary";
  fullWidth?: boolean;
}

const Button = ({
  children,
  variant = "primary",
  fullWidth = false,
  className = "",
  ...props
}: ButtonProps) => {
  const variants = {
    primary:
      "bg-blue-600 text-white hover:bg-blue-700",
    danger:
      "bg-red-500 text-white hover:bg-red-600",
    secondary:
      "bg-gray-200 text-gray-800 hover:bg-gray-300",
  };

  return (
    <button
      {...props}
      className={`
        rounded-lg px-4 py-2
        transition-all duration-200
        hover:scale-105 hover:shadow-md
        cursor-pointer
        active:scale-95
        disabled:cursor-not-allowed disabled:opacity-50
        ${fullWidth ? "w-full" : ""}
        ${variants[variant]}
        ${className}
      `}
    >
      {children}
    </button>
  );
};

export default Button;