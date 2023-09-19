import { H_OptionalAggregateNav } from './h-optional-aggregate-nav.entity';
import { Entity, PrimaryGeneratedColumn, Column, ManyToOne } from 'typeorm';

@Entity('h_multiple_dependent')
export class H_MultipleDependent {

  @PrimaryGeneratedColumn('uuid')
  id?: string;
  @Column()
  multipleDepAttr: string;

  @ManyToOne(() => H_OptionalAggregateNav, (h_OptionalAggregateNav) => h_OptionalAggregateNav.h_MultipleDependents, { cascade: ['insert', 'update'] })
  h_OptionalAggregateNav?: H_OptionalAggregateNav;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}