import { Entity, PrimaryGeneratedColumn, Column, Index } from 'typeorm';

@Entity('custom_index')
export class CustomIndex {

  @PrimaryGeneratedColumn('uuid')
  id?: string;
  @Column()
  @Index()
  indexField: string;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}